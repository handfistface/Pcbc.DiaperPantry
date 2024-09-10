namespace Pcbc.DiaperPantry.SheetIngestion.Objects
{
    public class ChildForm
    {
        public ChildForm()
        {
            Children = new List<Child>();
        }

        //#	Created	Link	1. First and Last Name	2. Address (line-1)	2. Address (line-2)	2. Address (city)	2. Address (state)	
        //2. Address (zip)	2. Address (country)	3. Your Phone Number	4. Your Email	5. Relationship to child	
        //6. Does this child/children reside with you?	
        //7. Name	8. Diaper style	9. Age	10. Size	11. Birthday	
        //12. Name	13. Diaper style	14. Age	15. Size	16. Birthday	
        //17. Name	18. Diaper style	19. Age	20. Size	21. Birthday	
        //22. Name	23. Diaper style	24. Age	25. Size	26. Birthday	
        //27. Name	28. Diaper style	29. Age	30. Size	31. Birthday	
        //32. Do you have any other needs?	33. Would you like someone for the Diaper Ministry to contact you?	34. Preferred contact	
        //35. Preferred day or time to contact	36. Would you like one of our pastors to contact you?	37. Preferred contact	
        //38. Preferred day or time to contact
        public Guid Id { get; set; }
        public string CreatedLink { get; set; }
        public string FirstAndLastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string RelationshipToChild { get; set; }
        public bool DoesChildResideInSameHousehold { get; set; }
        public List<Child> Children { get; set; }
        public string AnyOtherNeeds { get; set; }
        public bool DiaperMinistryContact { get; set; }
        public string PreferredMinistryContact { get; set; }
        public string PreferredTimeOfDayToContactForMinistry { get; set; }
        public bool PastorContact { get; set; }
        public string PreferredPastorContact { get; set; }
        public string PreferredTimeOfDayToContactForPastor { get; set; }
    }
}
