using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Neo4j
{
    public class Neo4jRepositoryBase
    {
        protected GraphClient client { get; set; }
        private Neo4jConnection neo4JConnection;
        public Neo4jRepositoryBase(IOptions<Neo4jConnection> setting)
        {
            neo4JConnection = setting.Value;
            Connect(neo4JConnection);
        }
        private void Connect(Neo4jConnection neo4JConnection)
        {
            if(client==null)
            {
                client = new GraphClient(new Uri($"{neo4JConnection.ServerURl}"), neo4JConnection.UserName, neo4JConnection.Password);
                client.Connect();
            }    
        }
    }
}
