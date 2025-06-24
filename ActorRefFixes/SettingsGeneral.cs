using Mutagen.Bethesda.Synthesis.Settings;

namespace ActorRefFixes
{
    public class SettingsGeneral
    {
        [SynthesisOrder]
        [SynthesisSettingName("Enable Cell XLCN Updates")]
        [SynthesisTooltip("This setting attempts to update the Location (XLCN) of cells, using their parent worldspace as a point of reference.\n\nDisabling this option may impact population of some NPC XLCN records, as some NPCs are associated with Cells that have no location.\n\nHowever, it should reduce the number of patched records significantly and therefore the number of masters in use.")]
        public bool enableCellLocationUpdates = true;

        [SynthesisOrder]
        [SynthesisSettingName("Enable NPC XLCN Updates")]
        [SynthesisTooltip("This setting attempts to update the Exit Location (XLCN) of Actor References based on the Persistent Location of their parent cell.\n\nIf the cell has no persistent location keyword associated with it, the ref will be skipped.")]
        public bool enableNPCPersistentLocationUpdates = true;

        public ActorUpdates actorUpdateSettings = new();
    }

    public class ActorUpdates
    {
        [SynthesisOrder]
        [SynthesisSettingName("Update Animal Refs")]
        public bool updateAnimals = true;

        [SynthesisOrder]
        [SynthesisSettingName("Update Creature Refs")]
        public bool updateCreatures = true;

        [SynthesisOrder]
        [SynthesisSettingName("Update Dwemer Refs")]
        public bool updateDwemer = true;

        [SynthesisOrder]
        [SynthesisSettingName("Update Humanoid Refs")]
        public bool updateHumanoids = true;

        [SynthesisOrder]
        [SynthesisSettingName("Update Undead Refs")]
        public bool updateUndead = true;
    }
}
