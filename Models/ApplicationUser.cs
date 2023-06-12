using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace RestaurMap.Models;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
}