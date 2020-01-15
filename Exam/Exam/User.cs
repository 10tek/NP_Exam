using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? DeletedDate { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
    }
}
