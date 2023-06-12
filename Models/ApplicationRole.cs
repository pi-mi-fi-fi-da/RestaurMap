using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace RestaurMap.Models;

[CollectionName("Roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{

}