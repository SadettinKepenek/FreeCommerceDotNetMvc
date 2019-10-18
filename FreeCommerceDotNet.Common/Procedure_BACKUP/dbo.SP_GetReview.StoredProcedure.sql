USE [u8206796_dbF1B]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetReview]    Script Date: 18.10.2019 21:57:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetReview]
	@ProductId int=NULL,
	@CustomerId int=NULL,
	@ReviewStatus bit=NULL,
	@LowerBoundLike int=NULL,
	@UpperBoundLike int=NULL,
	@LowerBoundDislike int=NULL,
	@UpperBoundDislike int=NULL,
	@PublishDate nvarchar=NULL,
	@ReviewId int=NULL
AS
BEGIN
	
	SET NOCOUNT ON;
	Select 	Review.ReviewId,
		Review.Title ReviewTitle,
		Review.Comment ReviewComment,
		Review.Rating ReviewRating,
	    Review.ProductId ProductId,
		Review.PublishDate ReviewPublishDate,
		Review.Status ReviewStatus,
		Review.LikeCount ReviewLike,
		Review.DislikeCount ReviewDislike,
		Customer.CustomerId,
		Customer.Firstname CustomerFirstname,
		Customer.Lastname CustomerLastname,
		Customer.Email CustomerEmail
	FROM Reviews AS Review
	LEFT JOIN Customers Customer ON Review.CustomerId=Customer.CustomerId
	WHERE (Review.ReviewId=@ReviewId OR @ReviewId IS NULL)
	AND (Review.ProductId=@ProductId OR @ProductId IS NULL)
	AND (Review.Status=@ReviewStatus OR @ReviewStatus IS NULL)
	AND (Review.CustomerId=@CustomerId OR @CustomerId IS NULL)
	AND (Review.LikeCount>@LowerBoundLike OR @LowerBoundLike IS NULL)
	AND (Review.LikeCount<@UpperBoundLike OR @UpperBoundLike IS NULL)
	AND (Review.DislikeCount>@LowerBoundDislike OR @LowerBoundDislike IS NULL)
	AND (Review.DislikeCount<@UpperBoundDislike OR @UpperBoundDislike IS NULL)
	AND (Review.PublishDate=@PublishDate OR @PublishDate IS NULL)
END
GO
