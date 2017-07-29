using System;
using System.Collections.Generic;

namespace GlassdoorAPI
{
    public class FeaturedReview
    {
        public string attributionURL { get; set; }
        public int id { get; set; }
        public bool currentJob { get; set; }
        public string reviewDateTime { get; set; }
        public string jobTitle { get; set; }
        public string location { get; set; }
        public string jobTitleFromDb { get; set; }
        public string headline { get; set; }
        public string pros { get; set; }
        public string cons { get; set; }
        public int overall { get; set; }
        public int overallNumeric { get; set; }
    }

    public class Image
    {
        public string src { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Ceo
    {
        public string name { get; set; }
        public string title { get; set; }
        public int numberOfRatings { get; set; }
        public int pctApprove { get; set; }
        public int pctDisapprove { get; set; }
        public Image image { get; set; }
    }

    public class Logo
    {
        public string normalUrl { get; set; }
        public string mediumUrl { get; set; }
        public string smallUrl { get; set; }
    }

    public class ParentEmployer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string relationshipDate { get; set; }
        public string relationship { get; set; }
        public bool isSunset { get; set; }
        public string sunsetMessage { get; set; }
        public Logo logo { get; set; }
    }

    public class Employer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string website { get; set; }
        public bool isEEP { get; set; }
        public bool exactMatch { get; set; }
        public string industry { get; set; }
        public int numberOfRatings { get; set; }
        public string squareLogo { get; set; }
        public string overallRating { get; set; }
        public string ratingDescription { get; set; }
        public string cultureAndValuesRating { get; set; }
        public string seniorLeadershipRating { get; set; }
        public string compensationAndBenefitsRating { get; set; }
        public string careerOpportunitiesRating { get; set; }
        public string workLifeBalanceRating { get; set; }
        public int recommendToFriendRating { get; set; }
        public int sectorId { get; set; }
        public string sectorName { get; set; }
        public int industryId { get; set; }
        public string industryName { get; set; }
        public FeaturedReview featuredReview { get; set; }
        public Ceo ceo { get; set; }
        public ParentEmployer parentEmployer { get; set; }
    }

    public class Response
    {
        public string attributionURL { get; set; }
        public int currentPageNumber { get; set; }
        public int totalNumberOfPages { get; set; }
        public int totalRecordCount { get; set; }
        public List<Employer> employers { get; set; }
    }

    [Serializable]
    public class DataModel
    {
        public bool success { get; set; }
        public string status { get; set; }
        public string jsessionid { get; set; }
        public Response response { get; set; }
    }
}
