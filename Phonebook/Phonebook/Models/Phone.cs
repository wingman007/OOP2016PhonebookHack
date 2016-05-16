using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phonebook.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}