using System;
using System.Collections.Generic;

namespace DatabaseHandler.Entity
{
    public class ClassRoom
    {
        public string Name { get; set; }
        public string RoomName { get; set; }
        public List<Student> Students { get; set; }
    }
}