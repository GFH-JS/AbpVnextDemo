﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Entities
{
    public class MongoTestEntity
    {
        public ObjectId id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
