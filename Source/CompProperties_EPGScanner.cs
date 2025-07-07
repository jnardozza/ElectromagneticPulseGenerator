using Verse;

namespace ElectromagneticPulseGenerator
{
    public class CompProperties_EPGScanner : CompProperties
    {
        public CompProperties_EPGScanner()
        {
            this.compClass = typeof(CompEPGScanner);
        }
        // Match vanilla Ground Penetrating Scanner values:
        // Mean time between finds, in in-game days (vanilla: 3)
        public float meanTimeBetweenFindsDays = 3f;
        // Maximum scan time in ticks before a guaranteed find (vanilla: 6 days * 60000 ticks/day = 360000)
        public int maxScanTimeTicks = 360000;
    }
}
