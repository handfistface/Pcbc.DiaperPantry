using Pcbc.DiaperPantry.SheetIngestion.Objects;
using Pcbc.DiaperPantry.SheetIngestion.Utility;
using System.Dynamic;

namespace Pcbc.DiaperPantry.SheetIngestion.Mapping
{
    public interface IDynamoToChildForm
    {
        ChildForm Map(ExpandoObject toMap);
        List<ChildForm> Map(IEnumerable<ExpandoObject> toMap);
    }

    public class DynamoToChildForm : IDynamoToChildForm
    {
        public List<ChildForm> Map(IEnumerable<ExpandoObject> toMap)
        {
            var mapped = new List<ChildForm>();
            foreach (ExpandoObject obj in toMap)
            {
                var mappedObject = Map(obj);
                mapped.Add(mappedObject);
            }
            return mapped;
        }

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
            var child1 = RetrieveChild(toMap, 0);
            if (child1 != null)
                childForm.Children.Add(child1);
            //12. Name	13. Diaper style	14. Age	15. Size	16. Birthday	
            var child2 = RetrieveChild(toMap, 1);
            if (child2 != null)
                childForm.Children.Add(child2);
            //17. Name	18. Diaper style	19. Age	20. Size	21. Birthday
            var child3 = RetrieveChild(toMap, 2);
            if (child3 != null)
                childForm.Children.Add(child3);
            //22. Name	23. Diaper style	24. Age	25. Size	26. Birthday
            var child4 = RetrieveChild(toMap, 3);
            if (child4 != null)
                childForm.Children.Add(child4);
            //27. Name	28. Diaper style	29. Age	30. Size	31. Birthday
            var child5 = RetrieveChild(toMap, 4);
            if (child5 != null)
                childForm.Children.Add(child5);

            //32. Do you have any other needs?	33. Would you like someone for the Diaper Ministry to contact you?	34. Preferred contact
            childForm.AnyOtherNeeds = ExpandoUtilities.GetByPropertyName(toMap, "32. Do you have any other needs?");
            childForm.DiaperMinistryContact = ParseYesNo(ExpandoUtilities.GetByPropertyName(toMap, "33. Would you like someone for the Diaper Ministry to contact you?"));
            childForm.PreferredMinistryContact = ExpandoUtilities.GetByPropertyName(toMap, "34. Preferred contact");

            //35. Preferred day or time to contact	36. Would you like one of our pastors to contact you?	37. Preferred contact	
            childForm.PreferredTimeOfDayToContactForMinistry = ExpandoUtilities.GetByPropertyName(toMap, "35. Preferred day or time to contact");
            childForm.PastorContact = ParseYesNo(ExpandoUtilities.GetByPropertyName(toMap, "33. Would you like someone for the Diaper Ministry to contact you?"));
            childForm.PreferredTimeOfDayToContactForPastor = ExpandoUtilities.GetByPropertyName(toMap, "37. Preferred contact");

            //38. Preferred day or time to contact
            childForm.PreferredTimeOfDayToContactForPastor = ExpandoUtilities.GetByPropertyName(toMap, "38. Preferred day or time to contact");

            return childForm;
        }

        /// <summary>
        ///     Parses the child out of the expando object
        /// </summary>
        /// <param name="toMap"></param>
        /// <param name="childCount">0 based child count</param>
        /// <returns></returns>
        private Child? RetrieveChild(ExpandoObject toMap, int childCount)
        {
            var firstChildIndex = 7;
            var numberOfFieldsInForm = 5;
            var startingIndex = childCount * numberOfFieldsInForm + firstChildIndex;

            var child1 = new Child();
            child1.Name = ExpandoUtilities.GetByPropertyName(toMap, $"{startingIndex++}. Name");
            child1.DiaperStyle = ExpandoUtilities.GetByPropertyName(toMap, $"{startingIndex++}. Diaper style");
            child1.Age = ParseInt(ExpandoUtilities.GetByPropertyName(toMap, $"{startingIndex++}. Age") 
                ?? "-1");
            child1.DiaperSize = ExpandoUtilities.GetByPropertyName(toMap, $"{startingIndex++}. Size");
            child1.Birthday = ParseDateTime(ExpandoUtilities.GetByPropertyName(toMap, $"{startingIndex++}. Birthday") 
                ?? DateTime.MinValue.ToString());

            if (string.IsNullOrEmpty(child1.Name) 
                && string.IsNullOrEmpty(child1.DiaperStyle) 
                && string.IsNullOrEmpty(child1.DiaperSize))
                return null;

            return child1;
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
