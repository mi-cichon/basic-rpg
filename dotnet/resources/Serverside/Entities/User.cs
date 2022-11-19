using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Serverside.Enums;
using System.ComponentModel;
using GTANetworkAPI;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Backend.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [DefaultValue(typeof(double), "0")]
        public double Money { get; set; }

        [DefaultValue(typeof(int), "0")]
        public int Experience { get; set; }

        [DefaultValue(typeof(int), "0")]
        public int Level { get; set; }

        public ulong CreatedBy { get; set; }


        [DefaultValue(typeof(Permissions), "0")]
        public Permissions Permissions { get; set; }

        public DateTime RegisteredDate { get; set; }

        internal string? _LastPosition { get; set; }

        [NotMapped]
        public Vector3? LastPosition
        {
            get 
            { 
                return _LastPosition == null 
                    ? null 
                    : JsonConvert.DeserializeObject<Vector3>(_LastPosition); 
            }
            set
            {
                _LastPosition = value == null
                    ? null
                    : JsonConvert.SerializeObject(value);

            }
        }
    }
}
