using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltyProgram
{
    public class UsersModule
    {
    }

    public class LoyaltyProgramUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LoyaltyPoints { get; set; }
        public LoyaltyProgramSettings Settings { get; set; }
    }

    public class LoyaltyProgramSettings
    {
        public string[] Interests { get; set; }
    }
}
