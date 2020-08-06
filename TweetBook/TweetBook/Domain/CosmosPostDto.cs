using Cosmonaut.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Domain
{
    public class CosmosPostDto
    {
        [CosmosPartitionKey]

        public string Id { get; set; }

        public string Name { get; set; }

    }
}
