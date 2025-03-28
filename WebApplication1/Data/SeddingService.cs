using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class SeddingService
    {
        private readonly WebApplication1Context _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeddingService(WebApplication1Context context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            Console.WriteLine("1");
            // Se já houver dados nos registros principais ou usuário, não realiza o seeding
            if (_context.Departament.Any()
                || _context.Seller.Any()
                || _context.SalesRecord.Any()
                || _context.Users.Any())
            {
                Console.WriteLine("1.5");
                return; // banco já semeado
            }

            // Seeding de Departamentos
            Departament d1 = new Departament(1, "Financeiro");
            Departament d2 = new Departament(2, "Vendas");
            Departament d3 = new Departament(3, "Eletrônicos");
            Departament d4 = new Departament(4, "Manutenções");

            // Seeding de Vendedores
            Seller s1 = new Seller(1, "Diego", "diego@gmail.system.com", 30000.00, DateTime.Parse("30/04/2005"), d2);
            Seller s2 = new Seller(2, "Sofia", "sofia@gmail.system.com", 1500.00, DateTime.Parse("30/04/2031"), d1);
            Seller s3 = new Seller(3, "Nico", "nico@gmail.system.com", 900.00, DateTime.Parse("30/04/2037"), d3);
            Seller s4 = new Seller(4, "Melissa", "melissa@gmail.system.com", 3000.00, DateTime.Parse("30/04/2050"), d4);

            // Seeding de Registros de Vendas
            SalesRecord sr1 = new SalesRecord(1, DateTime.Parse("30/01/2025"), 500000.00, Models.Enums.SalesStatus.Billed, s1);
            SalesRecord sr2 = new SalesRecord(2, DateTime.Parse("27/10/2025"), 5000.00, Models.Enums.SalesStatus.Billed, s2);
            SalesRecord sr3 = new SalesRecord(3, DateTime.Parse("22/12/2025"), 700.00, Models.Enums.SalesStatus.Billed, s3);
            SalesRecord sr4 = new SalesRecord(4, DateTime.Parse("23/03/2025"), 1000000.00, Models.Enums.SalesStatus.Billed, s4);

            _context.Departament.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRecord.AddRange(sr1, sr2, sr3, sr4);

            _context.SaveChanges();
            Console.WriteLine("2");

            // Seeding do usuário Admin via Identity
            var userEmail = "admin@system.com";
            if (!_context.Users.Any(u => u.Email == userEmail))
            {
                var user = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(user, "Admin@123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Erro ao criar usuário admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
                else
                {
                    Console.WriteLine("Usuário admin criado com sucesso.");
                }
            }
        }
    }
}
