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
            //((IDictionary<string, object>)returnValues[1])["10. Size"]
            var childForm = new ChildForm();
            childForm.Id = Guid.Parse(ExpandoUtilities.GetByPropertyName(toMap, "Id").ToString() ?? "");
            childForm.CreatedLink = ExpandoUtilities.GetByPropertyName(toMap, "Link") as string ?? "";
            childForm.FirstAndLastName = ExpandoUtilities.GetByPropertyName(toMap, "1. First and Last Name") as string ?? "";

            //THE COLUMNS ARE OFF FOR SOME REASON, FIX BEFORE TACKLING THIS

            return childForm;
        }
    }
}
