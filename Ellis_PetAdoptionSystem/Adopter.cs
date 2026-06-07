/*******************************************************************
* Name: Princess Ellis
* Date: June 7, 2026
* Assignment: SDC320 Week 4 Course Project - Database Implementation
*
* Class that stores adopter information.
*/
public class Adopter
{
    public int AdopterId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Adopter(int adopterId, string firstName, string lastName, string phone, string email)
    {
        AdopterId = adopterId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
    }

    public override string ToString()
    {
        return $"Adopter ID: {AdopterId}\nName: {FirstName} {LastName}\nPhone: {Phone}\nEmail: {Email}";
    }
}