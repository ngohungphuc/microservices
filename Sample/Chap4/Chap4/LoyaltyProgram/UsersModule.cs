using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace LoyaltyProgram
{
    public sealed class UsersModule : NancyModule
    {
        public UsersModule() : base("/users")
        {
            Post("/", _ =>
            {
                var newUser = this.Bind<LoyaltyProgramUser>();
                this.AddRegisteredUser(newUser);
                return this.CreatedResponse(newUser);
            });
        }
        private dynamic CreatedResponse(LoyaltyProgramUser newUser)
        {
            return Negotiate.WithStatusCode(HttpStatusCode.Created)
                .WithHeader("Location", $"{Request.Url.SiteBase}/users/{newUser.Id}").WithModel(newUser);
        }

        private void AddRegisteredUser(LoyaltyProgramUser newUser)
        {
            // store the newUser to a data store
        }
    }
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

