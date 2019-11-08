# Image-Cloud-Gallery

A web Image repository with a Vue.js frontend and a C# .netCore backend with integration to a SQL Server Database. It uses claims-based authorization to enable multiple users to safely upload and access their images without fear of another user getting access to them. It also allows for selection of images in the gallery for easy manipulation. Also supports bulk upload and deletion of images. A preview of the web application: <br> <br> 
![Application_Preview](./preview.png) <br>

## Running the application
### Backend: 
1. Set up your database as explained here: https://www.wikihow.com/Create-a-SQL-Server-Database (be sure to name the database "Memories") 
2. Copy and paste the deployment script (backend\Memories\Common\Database\PostDeploymentScript.sql) into the your database editor (I use SSMS for Sql Server Databases: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
3. Run the solution 

### Front-end 
1. Cd into the front-end folder
2. run the command "npm run serve"
3. The port on which the front-end end is running should now be diplayed in your terminal. 
4. Open that url in your browser and start playing around!

## Future Features: 
- Image sharing accross users
- Email notifications of shared images 
- Image recovery after deletion
- Drap and drop uploading
- Image recognition integration for classification and labeling of images 

## Improvements: 
- The re-load time of new images is slow right now, this could be improved by optimizing server traffic and only reloading the new uploaded images instead of the whole gallery on a new image upload. 
- Login page UI can be improved
- Adding a floating button for upload of new images 


