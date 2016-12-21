using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGSA.Model.ModelWrappers
{
    public class GenreWrapper : Utility.BindableBase
    {
        private dic_Genre dicGenre;
        public dic_Genre DicGenre
        {
            get { return dicGenre; }
            set { dicGenre = value; }
        }
        public GenreWrapper(dic_Genre g)
        {
            if(g == null)
            {
                dicGenre = new dic_Genre();
                return;
            }
            dicGenre = g;
            Name = g.name;
            GenreDescription = g.genre_description;
        }
        public int Id
        {
            get { return dicGenre.ID; }
            set { dicGenre.ID = value; Notify(); }
        }
        public string Name
        {
            get { return dicGenre.name; }
            set { dicGenre.name = value; Notify(); }
        }
        public string GenreDescription
        {
            get { return dicGenre.genre_description; }
            set { dicGenre.genre_description = value; Notify(); }
        }
        public int UpdateWho
        {
            get { return dicGenre.Update_Who; }
            set { dicGenre.Update_Who = value; }
        }
        public DateTime UpdateDate
        {
            get { return dicGenre.Update_Date; }
            set { dicGenre.Update_Date = value; }
        }

        public override string ToString()
        {
            if(Name != null)
            {
                return Name;
            }
            return "All/Any";
        }
    }
}
