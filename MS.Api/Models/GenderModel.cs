using System.Collections.Generic;

namespace MS.Api.Models
{
    public class GenderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClientModel> Clients { get; set; }

        public GenderModel()
        {
            Clients = new List<ClientModel>();
        }
    }
}