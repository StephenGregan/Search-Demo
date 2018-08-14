using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search.Models;
using Microsoft.Spatial;

namespace AzureSearchDemoII
{
    public class SearchableEvent
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public string[] Tags { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string[] Applications { get; set; }
        public string Location { get; set; }
        public GeographyPoint Geolocation { get; set; }
        public int? Rating { get; set; }

        public static IList<Field> GetSearchableEvents()
        {
            return new List<Field>()
            {
                new Field { Name = "key", Type = DataType.String, IsKey = true, IsSearchable = true, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true },
                new Field { Name = "name", Type = DataType.String, IsKey = false, IsSearchable = true, IsFilterable = false, IsFacetable = true, IsRetrievable = true, IsSortable = true  },
                new Field { Name = "category", Type = DataType.String, IsKey = false, IsSearchable = true, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true  },
                new Field { Name = "description", Type = DataType.String, IsKey = false, IsSearchable = true, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true  },
                new Field { Name = "location", Type = DataType.String, IsKey = false, IsSearchable = true, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true  },
                new Field { Name = "date", Type = DataType.DateTimeOffset, IsKey = false, IsSearchable = false, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true },
                new Field { Name = "tags", Type = DataType.Collection(DataType.String), IsKey = false, IsSearchable = true, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = false },
                new Field { Name = "geolocation", Type = DataType.GeographyPoint, IsKey = false, IsSearchable = false, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true },
                new Field { Name = "dateadded", Type = DataType.DateTimeOffset, IsKey = false, IsSearchable = false, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true },
                new Field { Name = "rating", Type = DataType.Int32, IsKey = false, IsSearchable = false, IsFilterable = true, IsFacetable = true, IsRetrievable = true, IsSortable = true }
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(string.Format("\tKey: {0}", this.Key));
            if (!string.IsNullOrWhiteSpace(this.Name)) { sb.Append(string.Format("\n\tName: {0}", this.Name)); }
            if (this.Date != null && this.Date != default(DateTimeOffset)) { sb.Append(string.Format("\n\tDate: {0}", this.Date)); }
            if (this.DateAdded != null && this.DateAdded != default(DateTimeOffset)) { sb.Append(string.Format("/n/tDate Added: {0}", this.DateAdded)); }
            if (!string.IsNullOrWhiteSpace(this.Category)) { sb.Append(string.Format("\n\tCategory: {0}", this.Category)); }
            if (!string.IsNullOrWhiteSpace(this.Description)) { sb.Append(string.Format("\n\tDescription: {0}", this.Description)); }
            if (!string.IsNullOrWhiteSpace(this.Location)) { sb.Append(string.Format("\n\tLocation: {0}", this.Location)); }
            if (this.Geolocation != null) { sb.Append(string.Format("\n\tGeo Location: {0}", this.Geolocation)); }
            if (this.Rating != null) { sb.Append(string.Format("\n\tRating: {0}", this.Rating)); }
            return sb.ToString();
        }
    }
}
