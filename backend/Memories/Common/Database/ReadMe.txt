To update entities from the database, run the following command: 
Scaffold-DbContext "Server=.\SQLExpress;Database=Memories;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Common/Database/Entities -f
