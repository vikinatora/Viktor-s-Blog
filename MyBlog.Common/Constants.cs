using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Common
{
    public static class Constants
    {
        public const string AdministratorRole = "Administrator";
        public const string ModeratorRole = "Moderator";
        public const string AdminArea = "Admin";


        public const int DefaultPage = 1;
        public const int DefaultPostsCountPerPage = 3;
        public const int MaximalHomePagePostBodyLength = 300;

        public const string FeedbackSentMessage = "Successfully sent feedback! Thank you. <3";
        public const string PostCreatedMessage = "Successfully created post with title {0}!";
        public const string CategoryCreatedMessage = "Successfully created category with name {0}!";
        public const string CategoryAlreadyExistsMessage = "Course with name {0} already exists";
        public const string UserPromotedMessage = "Successfully promoted user {0} to moderator!";
        public const string UserDemotedMessage = "Moderator privileges from user {0} successfully taken!";
        public const string UserAddedComment = "Successfully added a comment!";
        public const string ModeratorDeletedCommentMessage = "Successfully deleted  a comment!";

        public const string AvatarsPathFromWebRoot = @"\images\avatars";
        public const string AvatarName = "user_avatar_{0}";

        public const string CategoriesBannerPathFromWebRoot = @"\images\banners\";
        public const string CategoryBannerName = "{0}_banner{1}";
        public const string BannerUrlDisplayName = "Banner Url";




    }
}
