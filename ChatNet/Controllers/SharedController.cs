using Data.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace ChatNet.Controllers
{
    public class SharedController
    {
        public string? Id = null;
        public string? Name = null;
/*
        public static (string id, string name) SetConnected(int id, string name)
        {

            if (Id == null)
            {
                Id = id.ToString();
                Name = name;
            }
            else
            {
                Name = name;
            }
            return (Id, Name);
        }

        public static (string? id, string? name) GetConnected()
        {
            return (Id, Name);
        }

        public static void ClearConnected()
        {
            Id = null;
            Name = null;
        }*/
    }
}
