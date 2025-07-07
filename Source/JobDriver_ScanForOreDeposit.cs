using RimWorld;
using Verse;
using Verse.AI;
using System.Collections.Generic;

namespace ElectromagneticPulseGenerator
{
    public class JobDriver_ScanForOreDeposit : JobDriver
    {
        private Building_ElectromagneticPulseGenerator EPG => (Building_ElectromagneticPulseGenerator)job.targetA.Thing;
        private CompEPGScanner ScannerComp => EPG?.GetComp<CompEPGScanner>();

        public override bool TryMakePreToilReservations(bool errorOnFailed) => true;

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            this.FailOnBurningImmobile(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);

            var scan = new Toil();
            scan.initAction = () => scan.actor.pather.StopDead();
            scan.tickAction = () =>
            {
                scan.actor.skills?.Learn(SkillDefOf.Intellectual, 0.11f);
                scan.actor.GainComfortFromCellIfPossible();
                ScannerComp?.AddProgress(1); // Add 1 work per tick
            };
            scan.defaultCompleteMode = ToilCompleteMode.Never;
            // Progress bar: show progress toward guaranteed find
            scan.WithProgressBar(TargetIndex.A, () => ScannerComp != null ? (float)ScannerComp.GetScanProgress() : 0f);
            scan.activeSkill = () => SkillDefOf.Intellectual;
            yield return scan;
        }
    }
}
