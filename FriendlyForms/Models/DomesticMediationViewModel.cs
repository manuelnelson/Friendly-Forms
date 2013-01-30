using Models.ViewModels;

namespace FriendlyForms.Models
{
    public class DomesticMediationViewModel
    {
        public HouseViewModel HouseViewModel { get; set; }
        public PropertyViewModel PropertyViewModel { get; set; }
        public VehicleAllViewModel VehicleAllViewModel { get; set; }
        public DebtViewModel DebtViewModel { get; set; }
        public AssetViewModel AssetViewModel { get; set; }
        public HealthInsuranceViewModel HealthInsuranceViewModel { get; set; }
        public SpousalViewModel SpousalViewModel { get; set; }
        public TaxViewModel TaxViewModel { get; set; }
        public ChildSupportViewModel ChildSupportViewModel { get; set; }
        public FormsCompletedDomestic FormsCompleted { get; set; }
        public ParticipantViewModel ParticipantsViewModel { get; set; }
        public CourtViewModel CourtViewModel { get; set; }
        public bool HasChildren { get; set; }
        public int FormUserId { get; set; }
    }

    public class FormsCompletedDomestic
    {
        public bool RealEstateCompleted { get; set; }
        public bool VehicleCompleted { get; set; }
        public bool DebtCompleted { get; set; }
        public bool AssetCompleted { get; set; }
        public bool HealthCompleted { get; set; }
        public bool SpousalCompleted { get; set; }
        public bool TaxCompleted { get; set; }
        public bool ChildCompleted { get; set; }
    }
}