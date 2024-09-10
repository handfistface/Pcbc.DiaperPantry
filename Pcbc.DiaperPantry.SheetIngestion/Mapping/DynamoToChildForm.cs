using Pcbc.DiaperPantry.SheetIngestion.Objects;
using Pcbc.DiaperPantry.SheetIngestion.Utility;
using System.Dynamic;

namespace Pcbc.DiaperPantry.SheetIngestion.Mapping
{
    public interface IDynamoToChildForm
    {
        ChildForm Map(ExpandoObject toMap);
    }

    public class DynamoToChildForm : IDynamoToChildForm
    {
        public ChildForm Map(ExpandoObject toMap)
        {
            //((IDictionary<string, object>)returnValues[1])["10. Size"]
            var childForm = new ChildForm();
            childForm.Id = Guid.Parse(ExpandoUtilities.GetByPropertyName(toMap, "Id").ToString());
            childForm.CreatedLink = ExpandoUtilities.GetByPropertyName(toMap, "Link");

            //#	Created	Link	1. First and Last Name	2. Address (line-1)	2. Address (line-2)	2. Address (city)	2. Address (state)	
            childForm.FirstAndLastName = ExpandoUtilities.GetByPropertyName(toMap, "1. First and Last Name");
            childForm.AddressLine1 = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (line-1)");
            childForm.AddressLine2 = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (line-2)");
            childForm.City = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (city)");
            childForm.State = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (state)");

            //2. Address (zip)	2. Address (country)	3. Your Phone Number	4. Your Email	5. Relationship to child	
            childForm.Zip = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (zip)");
            childForm.Country = ExpandoUtilities.GetByPropertyName(toMap, "2. Address (country)");
            childForm.PhoneNumber = ExpandoUtilities.GetByPropertyName(toMap, "3. Your Phone Number");
            childForm.EmailAddress = ExpandoUtilities.GetByPropertyName(toMap, "4. Your Email");
            childForm.RelationshipToChild = ExpandoUtilities.GetByPropertyName(toMap, "5. Relationship to child");
            //6. Does this child/children reside with you?	
            childForm.DoesChildResideInSameHousehold = ParseYesNo(ExpandoUtilities.GetByPropertyName(toMap, "6. Does this child/children reside with you?"));

            //7. Name	8. Diaper style	9. Age	10. Size	11. Birthday
            var child1 = new Child();
            child1.Name = ExpandoUtilities.GetByPropertyName(toMap, "7. Name");
            child1.DiaperStyle = ExpandoUtilities.GetByPropertyName(toMap, "8. Diaper style");
            child1.Age = ParseInt(ExpandoUtilities.GetByPropertyName(toMap, "9. Age"));
            child1.DiaperSize = ExpandoUtilities.GetByPropertyName(toMap, "10. Size");
            child1.Birthday = ParseDateTime(ExpandoUtilities.GetByPropertyName(toMap, "11. Birthday"));
            childForm.Children.Add(child1);

            //12. Name	13. Diaper style	14. Age	15. Size	16. Birthday	
            //17. Name	18. Diaper style	19. Age	20. Size	21. Birthday	
            //22. Name	23. Diaper style	24. Age	25. Size	26. Birthday	
            //27. Name	28. Diaper style	29. Age	30. Size	31. Birthday	
            //32. Do you have any other needs?	33. Would you like someone for the Diaper Ministry to contact you?	34. Preferred contact	
            //35. Preferred day or time to contact	36. Would you like one of our pastors to contact you?	37. Preferred contact	
            //38. Preferred day or time to contact

            //THE COLUMNS ARE OFF FOR SOME REASON, FIX BEFORE TACKLING THIS

            return childForm;
        }

        private bool ParseYesNo(string answer)
        {
            answer = answer.Trim().ToLower();
            if (answer == "yes")
                return true;
            return false;
        }

        private int ParseInt(string toParse)
        {
            var parsed = 0;
            if (int.TryParse(toParse, out parsed))
                return parsed;
            return 0;
        }

        private DateTime ParseDateTime(string toParse)
        {
            var parsed = DateTime.MinValue;
            if (DateTime.TryParse(toParse, out parsed))
                return parsed;
            return DateTime.MinValue;
        }
    }
}
