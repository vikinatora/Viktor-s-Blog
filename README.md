# Viktor-s-Blog
## Introduction
### Viktor's Blog website is a project as a mandatory exam requirement for the course "C# MVC Frameworks - ASP.NET Core July 2018" by SoftUni.
## Description
The project is a personal blog website written with ASP.NET Core 2.1(using both MVC and Razor Pages). There are 3 roles - user, moderator and admin.Only the admin can create posts and categories for them. Normal users can leave feedback for the admin and leave comments under every post which the moderators can delete if they don't find them suitable. The admin has moderator privileges as well as an admin panel where he can promote/demote users to moderator, view each user's comments and ban users.
I will continue on expanding the blog as I continue to learn and maybe someday It will be published to the internet for everyone to visit. :)

## The public part is visible without authentication. It consists of:
* Main page of the website with all the posts and comments paginated by 3 with disabled commenting functionality
* Search form accessible from the main page
* Registration form
* Login form

## After successful login, registered users can access:
* Main page with all the posts with enabled commenting functionality
* Leave feedback for the website

## After being promoted to moderator, the user can access the moderator panel where he can:
* See all most recent comments
* Delete every comment

## The admin has administrative access to the system after successful login. In addition to all regular user abilities he has the following:
* Rights to create posts
* Rights to create categories
* Access to moderator panel
* Access to admin panel where he can:
  * Ban Users
  * View each user's comments
  * Promote/demote user to moderator
  * See all feedback messages

* **Admin functionality can be tested by logging in with Admin as username and admin as password by default

## Setting up
* Make sure MyBlog is set to be the default StartUp Project (Right click -> Set as default StartUp Project)
* Configure proper connection string for your server in OnConfiguring method in BlogContext
* Open PMC (Visual Studio -> Tools -> NuGet Package Manager -> Package Manager Console)
* Type `update-database`
* Start the Project


