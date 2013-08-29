namespace Models.ViewModels
{
    public enum Role
    {
        Admin =1,
        FirmAdmin,
        Lawyer,
        Default
    }
    public enum Gender
    {
        Male = 1,
        Female
    };
    public enum AuthorOfPlan
    {
        Parties = 1,
        Judge
    };

    public enum PlanType
    {
        NewPlan = 1,
        ExistingPlan,
        ExistingOrder
    };
    public enum DecisionMaker
    {
        Father = 1,
        Mother = 2,
        Both = 3,
        NotApplicable
    };
    public enum ParentRelationship
    {
        Mother = 1,
        Father,
        Plaintiff,
        Defendant
    };

    public enum CustodialParent
    {
        Primary = 1,
        NonCustodial,
        Joint
    };

    //This seems a bit dumb to start at 1, but doing this so that the default value of 0 doesn't highlight a button
    public enum YesNo
    {
        Yes = 1,
        No,
        Unknown
    };
    
    public enum HighLow
    {
        High = 1,
        Low,
        No
    };
    public enum TransportationCost
    {
        Own = 1,
        Half,
        Percentage,
        Other
    };

    public enum Thanksgiving
    {
        Before = 1,
        After,
        Other
    };
    public enum Christmas
    {
        AfterBreak = 1,
        Eve,
        Day,
        AfterDay,
        Other
    };

    public enum HolidayYear
    {
        Odd = 1,
        Even,
        Every,
        Never
    };


    public enum DateDetermination
    {
        Judge = 1,
        Parent
    };

    public enum ParentWeekends
    {
        EveryOther = 1, 
        FirstThird,
        FirstThirdFifth,
        SecondFourth,        
        Other
    };

    public enum PersonalProperty
    {
        AlreadyDivided = 1,
        Court,
        Other
    };
}
