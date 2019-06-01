
ALTER PROCEDURE FoodDelivery_FinalProject.MealTab
(
    @pageNum INT,
    @pageSize INT,
    @sortColumnName VARCHAR(50)
)
AS
BEGIN
  WITH PagingCTE AS
  (
    SELECT Name, MealCost,MainIngredient,SideIngredient,Drink, ROW_NUMBER() OVER 
     (ORDER BY CASE
	 when @sortcolumnName= 'Name' then [Name]
	 when @sortcolumnName= 'Main Ingredie nt' then MainIngredient
	 when @sortcolumnName= 'Side Ingredient' then SideIngredient
     when @sortcolumnName= 'Drink' then Drink

	 
	  END) AS RowNumber
    FROM FoodDelivery_FinalProject.Meal WITH(NOLOCK)
  )
  SELECT *
  FROM PagingCTE
  WHERE RowNumber BETWEEN (@pageNum - 1) * @pageSize + 1 
   AND @pageNum * @pageSize
END

EXEC FoodDelivery_FinalProject.MealTab
      @pageNum = 1, @pageSize = 10, @sortColumnName='Name'