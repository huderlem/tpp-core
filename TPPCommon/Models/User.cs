using MongoDB.Bson.Serialization.Attributes;
using TPPCommon.Persistence;

namespace TPPCommon.Models
{
    /// <summary>
    /// model for user objects.
    /// </summary>
    [Table(TableName)]
    public class User : Model
    {
        // Table name.
        public const string TableName = "users";

        // Field names.
        public const string ProvidedIdField = "provided_id";
        public const string ProvidedNameField = "provided_name";
        public const string NameField = "name";
        public const string SimpleNameField = "simple_name";
        public const string MoneyField = "money";

        /// <summary>
        /// unique id of the user.
        /// </summary>
        [BsonId]
        public readonly string Id;
        
        /// <summary>
        /// user id that the (chat) service, which this user originates from, provided.
        /// </summary>
        [BsonElement(ProvidedIdField)]
        public readonly string ProvidedId;
        
        /// <summary>
        /// name of the (chat) service this user originates from.
        /// </summary>
        [BsonElement(ProvidedNameField)]
        public readonly string ProvidedName;
        
        /// <summary>
        /// name of the user. this is how he is being displayed.
        /// </summary>
        [BsonElement(NameField)]
        public readonly string Name;
        
        /// <summary>
        /// simple name of this user. usually maps to lowercase-variations from irc. only contains ASCII.
        /// </summary>
        [BsonElement(SimpleNameField)]
        public readonly string SimpleName;

        /// <summary>
        /// current amount of money the user has.
        /// </summary>
        [BsonElement(MoneyField)]
        public readonly int Money;

        [BsonConstructor]
        public User(string id, string providedId, string name, string simpleName, string providedName, int money)
        {
            this.Id = id;
            this.ProvidedId = providedId;
            this.Name = name;
            this.SimpleName = simpleName;
            this.ProvidedName = providedName;
            this.Money = money;
        }
    }
}