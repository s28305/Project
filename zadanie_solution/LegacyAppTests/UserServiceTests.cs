using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Missing_FirstName()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser(null, null, "kowalski@wp.pl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Missing_At_Sign_And_Dot_In_Email()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Doe", "kowalskiwppl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Younger_Then_21_Years_Old()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Doe", "kowalski@wp.pl", new DateTime(2010, 1, 1), 1);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Malewski", "kowalski@wp.pl", new DateTime(1980, 1, 1), 2);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Smith", "smith@gmail.pl", new DateTime(1980, 1, 1), 3);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Kwiatkowski", "kwiatkowski@wp.pl", new DateTime(1980, 1, 1), 5);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_With_No_Credit_Limit()
    {
        //Arrange
        var service = new UserService();

        //Act
        var result = UserService.AddUser("John", "Kowalski", "kowalski@wp.pl", new DateTime(1980, 1, 1), 1);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_Does_Not_Exist()
    {
        //Arrange
        var service = new UserService();

        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = UserService.AddUser("John", "Unknown", "kowalski@wp.pl", new DateTime(1980, 1, 1), 100);
        });
    }
    
    [Fact]
    public void AddUser_Should_Throw_Exception_When_User_No_Credit_Limit_Exists_For_User()
    {
        //Arrange
        var service = new UserService();

        //Act and Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = UserService.AddUser("John", "Andrzejewicz", "andrzejewicz@wp.pl", new DateTime(1980, 1, 1), 6);
        });
    }
    
    [Fact]
    public void CheckCreditLimit_Should_Return_True_When_Credit_Limit_Too_Small()
    {
        // Arrange
        var creditData = new CreditData
        {
            HasCreditLimit = true,
            CreditLimit = 350
        };

        // Act
        var result = UserService.CheckCreditLimit(creditData);

        // Assert
        Assert.True(result);
    }
}