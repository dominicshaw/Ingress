using System;
using System.Data;

namespace Ingress.Data.DataSources
{
    public class Broker : IComparable
    {
        public int ID { get; }
        public string Name { get; }

        public DateTime InsertedAt { get; }

        public string Contact { get; }
        public string Email { get; }
        public bool LetterRequired { get; }
        public string FundManager { get; }
        public string Region { get; }
        public bool CSA { get; }
        public bool Deal { get; }
        public bool Cheque { get; }
        public bool ThirdParty { get; }
        public string BudgetSystemBrokerName { get; }
        public string ExchequerAccountNumber { get; }
        public string Misc { get; }

        public bool Deleted { get; }
        
        public bool Changed { get; }

        public Broker(int id, string nm)
        {
            ID = id;
            Name = nm;
        }

        public Broker(IDataRecord rdr)
        {
            ID                     = (int) rdr["RecipientID"];
            InsertedAt             = (DateTime)rdr["InsertedAt"];

            Name                   = rdr["RecipientName"].ToString();
            Contact                = NullChecker<string>.Check(rdr["RecipientContact"], string.Empty);
            Email                  = NullChecker<string>.Check(rdr["RecipientEMail"], string.Empty);
            LetterRequired         = NullChecker<bool>.Check(rdr["LetterRequired"]);
            FundManager            = NullChecker<string>.Check(rdr["FundManager"], string.Empty);
            Region                 = NullChecker<string>.Check(rdr["RecipientRegion"], string.Empty);
            CSA                    = NullChecker<bool>.Check(rdr["CSA"]);
            Deal                   = NullChecker<bool>.Check(rdr["Deal"]);
            Cheque                 = NullChecker<bool>.Check(rdr["Cheque"]);
            ThirdParty             = NullChecker<bool>.Check(rdr["ThirdParty"]);
            BudgetSystemBrokerName = NullChecker<string>.Check(rdr["BudgetSystemBrokerName"], string.Empty);
            ExchequerAccountNumber = NullChecker<string>.Check(rdr["ExchequerAccNo"], string.Empty);
            Misc                   = NullChecker<string>.Check(rdr["Misc"], string.Empty);

            Deleted                = (bool) rdr["Deleted"];

            Changed = false;
        }

        #region " IComparable "

        public static bool operator ==(Broker a, Broker b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return Equals(a.ID, b.ID);
        }

        public static bool operator !=(Broker a, Broker b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return false;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return true;

            return !Equals(a.ID, b.ID);
        }

        public int CompareTo(object obj)
        {
            var status = obj as Broker;

            return status == null ? 0 : ID.CompareTo(status.ID);
        }

        public override bool Equals(object obj)
        {
            var status = obj as Broker;
            return ID == status?.ID;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return ID.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
