using ColorexApp.Domain.Entities;
using ColorexApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ColorexApp.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedAsync(ColorexDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        // Seed Branches
        var laureles = Branch.Create("Sede Laureles", "LAU", "COLX-LAU", "1000", "1999");
        var tesoro = Branch.Create("Sede El Tesoro", "TES", "COLX-TES", "2000", "2999");
        var santaFe = Branch.Create("Sede Santa Fe", "SFE", "COLX-SFE", "3000", "3999");
        var unicentro = Branch.Create("Sede Unicentro", "UNI", "COLX-UNI", "4000", "4999");

        context.Branches.AddRange(laureles, tesoro, santaFe, unicentro);
        await context.SaveChangesAsync();

        // Seed Users
        var adminPw = BCrypt.Net.BCrypt.HashPassword("admin123");
        var managerPw = BCrypt.Net.BCrypt.HashPassword("manager123");
        var sellerPw = BCrypt.Net.BCrypt.HashPassword("seller123");

        var admin = User.Create("admin@colorex.com", adminPw, "Admin", "Colorex", UserRole.Admin, laureles.Id);
        var manager = User.Create("manager@colorex.com", managerPw, "Carlos", "Mendez", UserRole.Manager, laureles.Id);
        var seller = User.Create("seller@colorex.com", sellerPw, "Ana", "Lopez", UserRole.Seller, tesoro.Id);

        context.Users.AddRange(admin, manager, seller);
        await context.SaveChangesAsync();

        // Seed Products
        var camisa = Product.Create("Camisa Lino Colorex", "CAM-LINO-001", 89900m, 45000m);
        camisa.AddVariant("S", "Blanco", "#FFFFFF", 50);
        camisa.AddVariant("M", "Blanco", "#FFFFFF", 80);
        camisa.AddVariant("L", "Blanco", "#FFFFFF", 60);
        camisa.AddVariant("XL", "Blanco", "#FFFFFF", 40);
        camisa.AddVariant("S", "Azul", "#1E40AF", 30);
        camisa.AddVariant("M", "Azul", "#1E40AF", 50);
        camisa.AddVariant("L", "Azul", "#1E40AF", 40);
        camisa.AddVariant("XL", "Azul", "#1E40AF", 20);
        camisa.AddVariant("S", "Beige", "#F5F5DC", 25);
        camisa.AddVariant("M", "Beige", "#F5F5DC", 45);
        camisa.AddVariant("L", "Beige", "#F5F5DC", 35);
        camisa.AddVariant("XL", "Beige", "#F5F5DC", 15);

        var jean = Product.Create("Jean Fit Premium", "JEA-FIT-002", 149900m, 75000m);
        jean.AddVariant("28", "Negro", "#000000", 40);
        jean.AddVariant("30", "Negro", "#000000", 60);
        jean.AddVariant("32", "Negro", "#000000", 55);
        jean.AddVariant("34", "Negro", "#000000", 35);
        jean.AddVariant("36", "Negro", "#000000", 20);
        jean.AddVariant("28", "Azul Oscuro", "#1E3A5F", 30);
        jean.AddVariant("30", "Azul Oscuro", "#1E3A5F", 50);
        jean.AddVariant("32", "Azul Oscuro", "#1E3A5F", 45);
        jean.AddVariant("34", "Azul Oscuro", "#1E3A5F", 25);
        jean.AddVariant("36", "Azul Oscuro", "#1E3A5F", 15);

        var vestido = Product.Create("Vestido Floral", "VES-FLO-003", 129900m, 60000m);
        vestido.AddVariant("S", "Rojo", "#DC2626", 20);
        vestido.AddVariant("M", "Rojo", "#DC2626", 35);
        vestido.AddVariant("L", "Rojo", "#DC2626", 25);
        vestido.AddVariant("S", "Verde", "#16A34A", 15);
        vestido.AddVariant("M", "Verde", "#16A34A", 30);
        vestido.AddVariant("L", "Verde", "#16A34A", 20);
        vestido.AddVariant("S", "Amarillo", "#EAB308", 10);
        vestido.AddVariant("M", "Amarillo", "#EAB308", 20);
        vestido.AddVariant("L", "Amarillo", "#EAB308", 15);

        var chaqueta = Product.Create("Chaqueta Cuero", "CHA-CUE-004", 289900m, 145000m);
        chaqueta.AddVariant("M", "Negro", "#000000", 25);
        chaqueta.AddVariant("L", "Negro", "#000000", 35);
        chaqueta.AddVariant("XL", "Negro", "#000000", 20);
        chaqueta.AddVariant("M", "Marrón", "#8B4513", 15);
        chaqueta.AddVariant("L", "Marrón", "#8B4513", 25);
        chaqueta.AddVariant("XL", "Marrón", "#8B4513", 15);

        var blusa = Product.Create("Blusa Seda", "BLU-SED-005", 79900m, 38000m);
        blusa.AddVariant("S", "Blanco", "#FFFFFF", 40);
        blusa.AddVariant("M", "Blanco", "#FFFFFF", 60);
        blusa.AddVariant("L", "Blanco", "#FFFFFF", 40);
        blusa.AddVariant("S", "Rosa", "#EC4899", 25);
        blusa.AddVariant("M", "Rosa", "#EC4899", 45);
        blusa.AddVariant("L", "Rosa", "#EC4899", 30);
        blusa.AddVariant("S", "Lavanda", "#D8B4FE", 20);
        blusa.AddVariant("M", "Lavanda", "#D8B4FE", 35);
        blusa.AddVariant("L", "Lavanda", "#D8B4FE", 25);

        var pantalon = Product.Create("Pantalón Dril", "PAN-DRI-006", 99900m, 48000m);
        pantalon.AddVariant("30", "Beige", "#F5F5DC", 30);
        pantalon.AddVariant("32", "Beige", "#F5F5DC", 45);
        pantalon.AddVariant("34", "Beige", "#F5F5DC", 30);
        pantalon.AddVariant("30", "Gris", "#6B7280", 25);
        pantalon.AddVariant("32", "Gris", "#6B7280", 40);
        pantalon.AddVariant("34", "Gris", "#6B7280", 25);

        var falda = Product.Create("Falda Plisada", "FAL-PLI-007", 69900m, 32000m);
        falda.AddVariant("S", "Negro", "#000000", 30);
        falda.AddVariant("M", "Negro", "#000000", 45);
        falda.AddVariant("L", "Negro", "#000000", 30);
        falda.AddVariant("S", "Azul Marino", "#1E3A5F", 20);
        falda.AddVariant("M", "Azul Marino", "#1E3A5F", 35);
        falda.AddVariant("L", "Azul Marino", "#1E3A5F", 25);

        var saco = Product.Create("Saco Cardigan", "SAC-CAR-008", 119900m, 55000m);
        saco.AddVariant("S", "Gris", "#6B7280", 20);
        saco.AddVariant("M", "Gris", "#6B7280", 35);
        saco.AddVariant("L", "Gris", "#6B7280", 25);
        saco.AddVariant("XL", "Gris", "#6B7280", 15);
        saco.AddVariant("S", "Verde Oliva", "#4A7C59", 15);
        saco.AddVariant("M", "Verde Oliva", "#4A7C59", 25);
        saco.AddVariant("L", "Verde Oliva", "#4A7C59", 20);

        var shortJean = Product.Create("Short Jean", "SHO-JEA-009", 59900m, 28000m);
        shortJean.AddVariant("28", "Azul Claro", "#93C5FD", 30);
        shortJean.AddVariant("30", "Azul Claro", "#93C5FD", 45);
        shortJean.AddVariant("32", "Azul Claro", "#93C5FD", 35);
        shortJean.AddVariant("28", "Blanco", "#FFFFFF", 25);
        shortJean.AddVariant("30", "Blanco", "#FFFFFF", 35);
        shortJean.AddVariant("32", "Blanco", "#FFFFFF", 25);

        var camiseta = Product.Create("Camiseta Algodón", "CAM-ALG-010", 39900m, 18000m);
        camiseta.AddVariant("S", "Blanco", "#FFFFFF", 60);
        camiseta.AddVariant("M", "Blanco", "#FFFFFF", 100);
        camiseta.AddVariant("L", "Blanco", "#FFFFFF", 80);
        camiseta.AddVariant("XL", "Blanco", "#FFFFFF", 50);
        camiseta.AddVariant("S", "Negro", "#000000", 40);
        camiseta.AddVariant("M", "Negro", "#000000", 80);
        camiseta.AddVariant("L", "Negro", "#000000", 60);
        camiseta.AddVariant("XL", "Negro", "#000000", 35);

        context.Products.AddRange(camisa, jean, vestido, chaqueta, blusa, pantalon, falda, saco, shortJean, camiseta);
        await context.SaveChangesAsync();

        // Seed Branch Inventories
        var allVariants = await context.ProductVariants.ToListAsync();
        var random = new Random();
        foreach (var variant in allVariants)
        {
            var laurelesInv = BranchInventory.Create(laureles.Id, variant.Id, random.Next(5, 30));
            var tesoroInv = BranchInventory.Create(tesoro.Id, variant.Id, random.Next(5, 25));
            context.BranchInventories.AddRange(laurelesInv, tesoroInv);
        }
        await context.SaveChangesAsync();

        // Seed Transfers
        var camisaBlancaM = allVariants.First(v => v.Sku == "CAM-LINO-001-BLANCO-M");
        var jeanNegro32 = allVariants.First(v => v.Sku == "JEA-FIT-002-NEGRO-32");
        var blusaBlancaM = allVariants.First(v => v.Sku == "BLU-SED-005-BLANCO-M");
        var camisetaBlancaM = allVariants.First(v => v.Sku == "CAM-ALG-010-BLANCO-M");
        var vestidoRojoM = allVariants.First(v => v.Sku == "VES-FLO-003-ROJO-M");

        var transfer1 = InventoryTransfer.Create(laureles.Id, tesoro.Id, camisaBlancaM.Id, 20);
        transfer1.MarkInTransit();

        var transfer2 = InventoryTransfer.Create(laureles.Id, tesoro.Id, jeanNegro32.Id, 15);
        transfer2.MarkInTransit();

        var transfer3 = InventoryTransfer.Create(laureles.Id, santaFe.Id, blusaBlancaM.Id, 25);

        var transfer4 = InventoryTransfer.Create(tesoro.Id, laureles.Id, camisetaBlancaM.Id, 30);
        transfer4.MarkInTransit();

        var transfer5 = InventoryTransfer.Create(laureles.Id, unicentro.Id, vestidoRojoM.Id, 12);

        var transfer6 = InventoryTransfer.Create(laureles.Id, tesoro.Id, camisaBlancaM.Id, 10);
        transfer6.MarkReceived(10);

        var transfer7 = InventoryTransfer.Create(laureles.Id, tesoro.Id, jeanNegro32.Id, 8);
        transfer7.MarkReceived(8);

        context.InventoryTransfers.AddRange(transfer1, transfer2, transfer3, transfer4, transfer5, transfer6, transfer7);
        await context.SaveChangesAsync();
    }
}
