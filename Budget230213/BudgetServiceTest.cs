using NSubstitute;

namespace Budget230213;

public class BudgetServiceTest
{
    private readonly IBudgetRepository _repository;

    private readonly BudgetService _target;

    public BudgetServiceTest()
    {
        _repository = Substitute.For<IBudgetRepository>();
        _target = new BudgetService(_repository);
    }

    [Fact]
    public void OneDayBudget()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 2, 2),
            new DateTime(2023, 2, 2));
        Assert.Equal(100, actual);
    }

    [Fact]
    public void OneMonth()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202311",
                Amount = 3000
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 11, 28),
            new DateTime(2023, 11, 30));
        Assert.Equal(300, actual);
    }

    [Fact]
    public void TwoDayBudgetCrossTwoMonths()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            },
            new()
            {
                YearMonth = "202303",
                Amount = 31000
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 2, 28),
            new DateTime(2023, 3, 2));
        Assert.Equal(2100, actual);
    }

    [Fact]
    public void start_without_budget()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 1, 28),
            new DateTime(2023, 2, 2));
        Assert.Equal(200, actual);
    }

    [Fact]
    public void end_without_budget()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 2, 26),
            new DateTime(2023, 3, 2));
        Assert.Equal(300, actual);
    }

    [Fact]
    public void BudgetCrossThreeMonths()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            },
            new()
            {
                YearMonth = "202303",
                Amount = 31000
            },
            new()
            {
                YearMonth = "202304",
                Amount = 30
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 2, 28),
            new DateTime(2023, 4, 2));
        Assert.Equal(31102, actual);
    }

    [Fact]
    public void OneMonthNoBudget()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202302",
                Amount = 2800
            },
            new()
            {
                YearMonth = "202303",
                Amount = 0
            },
            new()
            {
                YearMonth = "202304",
                Amount = 30
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 2, 28),
            new DateTime(2023, 4, 2));
        Assert.Equal(102, actual);
    }

    [Fact]
    public void BudgetCrossYear()
    {
        _repository.GetAll().Returns(new List<Budget>
        {
            new()
            {
                YearMonth = "202311",
                Amount = 3000
            },
            new()
            {
                YearMonth = "202312",
                Amount = 0
            },
            new()
            {
                YearMonth = "202401",
                Amount = 31
            }
        });
        var actual = _target.Query(
            new DateTime(2023, 11, 28),
            new DateTime(2024, 1, 2));
        Assert.Equal(302, actual);
    }
}