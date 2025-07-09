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
        public int maxScanTimeTicks = 180000; // 3 days * 60000 ticks/day
    }
}
