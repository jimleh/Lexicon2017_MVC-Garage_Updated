namespace MVCGarage_Updated.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleID = c.Int(nullable: false, identity: true),
                        VehicleRegNum = c.String(nullable: false),
                        VehicleOwner = c.String(nullable: false),
                        VehicleType = c.Int(nullable: false),
                        VehicleSize = c.Int(nullable: false),
                        VehicleDateParked = c.String(nullable: false),
                        VehicleParkingSpot = c.Int(nullable: false),
                        VehicleFee = c.Double(nullable: false),
                        VehicleDateCheckout = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.VehicleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vehicles");
        }
    }
}
