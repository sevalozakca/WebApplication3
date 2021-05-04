using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.Models.Data_Access.Entities;
using WebApplication3.Models.Data_Access.Repositories;

namespace WebApplication3.Controllers
{
    public class MembersController : Controller
    {
        MemberRepository memberRepository;
        private readonly ECommercialWebSiteDbContext _context;


        public MembersController(ECommercialWebSiteDbContext context)
        {
            _context = context;
            memberRepository = new MemberRepository(context);
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            return View(await _context.Members.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,Name,LastName,UserName,EMail")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,Name,LastName,UserName,EMail")] Member member)
        {
            if (id != member.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberID == id);
        }


        public IActionResult Login(Member member)
        {

            HttpContext.Session.SetString("MemberID", member.MemberID.ToString());
            return View();
        }

        public IActionResult Log(Member member)
        {
            Member registered = memberRepository.GetMemberByEmail(member.EMail);
            HttpContext.Session.SetString("MemberID", registered.MemberID.ToString());
            return RedirectToAction("Index2", "Home");
        }

        [HttpPost]
        public IActionResult Mail(Member member)
        {

            try
            {
                Member newMember = memberRepository.createMember(member.EMail);
                memberRepository.AddMember(newMember);

                HttpContext.Session.SetString("MemberID", newMember.MemberID.ToString());

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("sevalozakca@gmail.com", "");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage();
                msj.From = new MailAddress(member.EMail);
                msj.To.Add("sevalozakca@gmail.com");
                msj.Subject = member.EMail + "" + "";
                msj.Body = "Merhaba";
                client.Send(msj);

                MailMessage mesaj2 = new MailMessage();
                mesaj2.From = new MailAddress(member.EMail, "Merhaba!");
                mesaj2.To.Add(member.EMail);
                mesaj2.Subject = "Hesap aktivasyonu";
                mesaj2.IsBodyHtml = true;
                mesaj2.Body += "Sisteme kayıt olduğunuz için teşekkür ederiz, artık siz de Gittigidiyor'da bir kullanıcısınız, Email adresiniz ile siteye giriş yapabilirsiniz." + "<a href=\'http://localhost:49269/\'Blog Giriş</a>";

                client.Send(mesaj2);
                //ViewBag.Succes="Mailiniz başarı ile gönderildi!";
                ViewData["error"] = "Mailiniz başarı ile gönderildi!";
            }
            catch (Exception e)
            {

                throw;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
