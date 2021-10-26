# Crud-opertion-after-connection-with-microsoft-dynamics-365
Crud opertion after connection with microsoft dynamics 365
another code
////
[3:47 AM] Muhammad Shahzaib
/// create new record Entity contact = new Entity("contact");
contact["firstname"] = "Nawal";
contact["lastname"] = "Ijaz";
Guid contactId = svc.Create(contact);
Console.WriteLine("New contact id: {0} .", contactId.ToString());
Entity retrievedContact = svc.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
Console.WriteLine("Record retrieved {0}", retrievedContact.Id.ToString()); // Update record using Id, retrieve all attributes
Entity updatedContact = new Entity("contact");
updatedContact = svc.Retrieve(contact.LogicalName, contactId, new ColumnSet(true));
updatedContact["jobtitle"] = "CEO";
updatedContact["emailaddress1"] = "test@test.com";
svc.Update(updatedContact);
Console.WriteLine("Updated contact"); // Retrieve specific fields using ColumnSet
ColumnSet attributes = new ColumnSet(new string[] { "jobtitle", "emailaddress1" });
retrievedContact = svc.Retrieve(contact.LogicalName, contactId, attributes);
foreach (var a in retrievedContact.Attributes)
{
Console.WriteLine("Retrieved contact field {0} - {1}", a.Key, a.Value);
} // Delete a record using Id
svc.Delete(contact.LogicalName, contactId);
Console.WriteLine("Deleted");
Console.ReadLine();

[5:41 AM] Muhammad Shahzaib
QueryExpression qe = new QueryExpression("contact");
qe.ColumnSet = new ColumnSet("firstname", "lastname");
EntityCollection ec = svc.RetrieveMultiple(qe);
for (int i = 0; i < ec.Entities.Count; i++)
{
if (ec.Entities[i].Attributes.ContainsKey("firstname"))
{
Console.WriteLine(ec.Entities[i].Attributes["firstname"] + "\n");
Console.WriteLine(ec.Entities[i].Attributes["lastname"] + "\n");
Console.WriteLine(ec.Entities[i].Attributes["jobtitle"] + "\n");
Console.WriteLine(ec.Entities[i].Attributes["emailaddress1"] + "\n"); }
}

