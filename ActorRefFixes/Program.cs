using Mutagen.Bethesda;
using Mutagen.Bethesda.FormKeys.SkyrimSE;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Synthesis;

namespace ActorRefFixes
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            return await SynthesisPipeline.Instance
                .AddPatch<ISkyrimMod, ISkyrimModGetter>(RunPatch)
                .SetTypicalOpen(GameRelease.SkyrimSE, "YourPatcher.esp")
                .Run(args);
        }

        public static void PatchCells(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            foreach (var cell in state.LoadOrder.PriorityOrder.Cell().WinningContextOverrides(state.LinkCache))
            {
                var cellLocation = cell.Record.Location;

                if (cellLocation.IsNull)
                {
                    if (cell.TryGetParentContext<IWorldspace, IWorldspaceGetter>(out var worldspaceContext))
                    {
                        var worldspaceLocation = worldspaceContext.Record.Location;

                        if (!worldspaceLocation.IsNull)
                        {
                            var p = cell.GetOrAddAsOverride(state.PatchMod);
                            p.Location.SetTo(worldspaceLocation);
                        }
                    }
                }
            }
        }
        public static void PatchActorRefs(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            foreach (var actorReference in state.LoadOrder.PriorityOrder.PlacedNpc().WinningContextOverrides(state.LinkCache))
            {
                if (actorReference.Record.VirtualMachineAdapter is null)
                {
                    var persistentLocation = actorReference.Record.PersistentLocation;
                    if (persistentLocation.IsNull)
                    {
                        if (actorReference.TryGetParentContext<ICell, ICellGetter>(out var cContext))
                        {
                            var cellLocation = cContext.Record.Location;

                            if (!cellLocation.IsNull)
                            {
                                var p = actorReference.GetOrAddAsOverride(state.PatchMod);
                                p.PersistentLocation.SetTo(cellLocation);
                            }
                            else
                            {
                                if (actorReference.TryGetParentContext<ICell, ICellGetter>(out var wContext))
                                {
                                    var wLocation = wContext.Record.Location;
                                    if (!wLocation.IsNull) {
                                        var p = actorReference.GetOrAddAsOverride(state.PatchMod);
                                        p.PersistentLocation.SetTo(wLocation);
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void RunPatch(IPatcherState<ISkyrimMod, ISkyrimModGetter> state)
        {
            PatchCells(state);
            PatchActorRefs(state);
        }
    }
}
