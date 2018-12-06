using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    [Serializable]
    public class Settings
    {
        private String loginPreference;
        private String theme;
        private String profileInfoPrivacy;
        private String photoPrivacy;
        private String personalContactInfoPrivacy;

        public String LoginPreference
        {
            get { return loginPreference; }
            set { loginPreference = value; }
        }
        public String Theme
        {
            get { return theme; }
            set { theme = value; }
        }
        public String ProfileInfoPrivacy
        {
            get { return profileInfoPrivacy; }
            set { profileInfoPrivacy = value; }
        }
        public String PhotoPrivacy
        {
            get { return photoPrivacy; }
            set { photoPrivacy = value; }
        }
        public String PersonalContactInfoPrivacy
        {
            get { return personalContactInfoPrivacy; }
            set { personalContactInfoPrivacy = value; }
        }
    }
}
