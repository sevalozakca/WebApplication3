using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Data_Access.Entities;

namespace WebApplication3.Models.Data_Access.Repositories
{
    public class MemberRepository
    {
        ECommercialWebSiteDbContext ecommercialwebSiteDbContext;
        public MemberRepository(ECommercialWebSiteDbContext context)
        {
            ecommercialwebSiteDbContext = context;
        }

        public void AddMember(Member member)
        {
            if (member.EMail == null)
            {
                throw new Exception();
            }
            ecommercialwebSiteDbContext.Add(member);
            ecommercialwebSiteDbContext.SaveChanges();
        }


        public Member GetMemberByEmail(string email)
        {
            var member = ecommercialwebSiteDbContext.Members.Where(a => a.EMail == email).FirstOrDefault();
            return member;
        }


        //create member with e-mail
        public Member createMember(string email)
        {
            //e-mail check
            EmailExist(email);

            Member member = new Member();
            Random rnd = new Random();
            int nmbr = rnd.Next();

            member.EMail = email;
            member.Name = Name(email);
            return member;

        }

        //e-mail check
        public void EmailExist(string email)
        {
            var mail = ecommercialwebSiteDbContext.Members.Where(a => a.EMail == email).FirstOrDefault();
            if (mail != null)
            {
                throw new Exception();
            }
        }

        //method for name
        public string Name(string email)
        {
            string name = string.Empty;
            foreach (char item in email)
            {
                if (item != '@')
                {
                    if (item != '-')
                    {
                        if (item != '.')
                        {
                            if (item != '*')
                            {
                                name += item;
                            }

                        }

                    }

                }
                else
                {
                    break;
                }
            }
            return name;
        }

    }
}
