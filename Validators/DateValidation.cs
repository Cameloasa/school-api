using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Validators;

public class DateValidation
{
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;

    public DateValidation(DateTime startDate, DateTime endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public List<string> Validate()
    {
        var errors = new List<string>();

        if (_startDate < DateTime.Today)
            errors.Add("Start date cannot be in the past.");

        if (_endDate <= _startDate)
            errors.Add("End date must be later than start date.");

        return errors;
    }
}

