using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Acme.BookStore.Dtos.MongoTest
{
    public class GetMongoTestDto
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
