/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT * FROM [dbo].[Country])
BEGIN
    INSERT INTO [dbo].[Country] ([Name]) VALUES (N'United States');
    INSERT INTO [dbo].[Country] ([Name]) VALUES (N'Canada');
END

IF NOT EXISTS (SELECT * FROM [dbo].[StateProvince])
BEGIN
    -- US States
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Alabama', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Alaska', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Arizona', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Arkansas', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'California', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Colorado', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Connecticut', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Delaware', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Florida', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Georgia', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Hawaii', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Idaho', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Illinois', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Indiana', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Iowa', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Kansas', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Kentucky', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Louisiana', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Maine', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Maryland', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Massachusetts', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Michigan', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Minnesota', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Mississippi', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Missouri', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Montana', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Nebraska', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Nevada', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New Hampshire', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New Jersey', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New Mexico', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New York', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'North Carolina', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'North Dakota', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Ohio', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Oklahoma', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Oregon', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Pennsylvania', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Rhode Island', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'South Carolina', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'South Dakota', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Tennessee', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Texas', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Utah', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Vermont', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Virginia', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Washington', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'West Virginia', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Wisconsin', 1);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Wyoming', 1);

    -- Canadian Provinces
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Alberta', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'British Columbia', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Manitoba', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'New Brunswick', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Newfoundland and Labrador', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Nova Scotia', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Ontario', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Prince Edward Island', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Quebec', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Saskatchewan', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Northwest Territories', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Nunavut', 2);
    INSERT INTO [dbo].[StateProvince] ([Name], [CountryId]) VALUES (N'Yukon', 2);
END

IF NOT EXISTS (SELECT * FROM [dbo].[City])
BEGIN
    -- US State Capitals
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Montgomery', 1);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Juneau', 2);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Phoenix', 3);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Little Rock', 4);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Sacramento', 5);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Denver', 6);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Hartford', 7);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Dover', 8);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Tallahassee', 9);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Atlanta', 10);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Honolulu', 11);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Boise', 12);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Springfield', 13);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Indianapolis', 14);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Des Moines', 15);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Topeka', 16);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Frankfort', 17);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Baton Rouge', 18);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Augusta', 19);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Annapolis', 20);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Boston', 21);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Lansing', 22);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Saint Paul', 23);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Jackson', 24);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Jefferson City', 25);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Helena', 26);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Lincoln', 27);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Carson City', 28);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Concord', 29);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Trenton', 30);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Santa Fe', 31);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Albany', 32);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Raleigh', 33);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Bismarck', 34);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Columbus', 35);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Oklahoma City', 36);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Salem', 37);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Harrisburg', 38);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Providence', 39);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Columbia', 40);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Pierre', 41);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Nashville', 42);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Austin', 43);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Salt Lake City', 44);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Montpelier', 45);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Richmond', 46);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Olympia', 47);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Charleston', 48);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Madison', 49);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Cheyenne', 50);

    -- Canadian Province Capitals
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Edmonton', 51);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Victoria', 52);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Winnipeg', 53);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Fredericton', 54);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'St. John''s', 55);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Halifax', 56);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Toronto', 57);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Charlottetown', 58);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Quebec City', 59);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Regina', 60);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Yellowknife', 61);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Iqaluit', 62);
    INSERT INTO [dbo].[City] ([Name], [StateProvinceId]) VALUES (N'Whitehorse', 63);
END

IF NOT EXISTS (SELECT * FROM [dbo].[Product])
BEGIN
    INSERT INTO [dbo].[Product] ([Name], [Description], [Price], [ImageUrl]) VALUES 
        (N'Laptop', N'High performance laptop', 1200.00, N'https://example.com/laptop.jpg'),
        (N'Smartphone', N'Latest model smartphone', 800.00, N'https://example.com/smartphone.jpg'),
        (N'Headphones', N'Noise-cancelling headphones', 150.00, N'https://example.com/headphones.jpg'),
        (N'Wireless Mouse', N'Ergonomic wireless mouse', 35.99, N'https://example.com/mouse.jpg'),
        (N'Mechanical Keyboard', N'RGB backlit mechanical keyboard', 89.50, N'https://example.com/keyboard.jpg'),
        (N'4K Monitor', N'Ultra HD 27-inch monitor', 399.99, N'https://example.com/monitor.jpg'),
        (N'Webcam', N'1080p HD webcam', 59.99, N'https://example.com/webcam.jpg'),
        (N'USB-C Hub', N'7-in-1 USB-C hub', 49.99, N'https://example.com/hub.jpg'),
        (N'External SSD', N'1TB portable SSD', 129.99, N'https://example.com/ssd.jpg'),
        (N'Bluetooth Speaker', N'Portable Bluetooth speaker', 79.99, N'https://example.com/speaker.jpg');
END

IF NOT EXISTS (SELECT * FROM [dbo].[User])
BEGIN
    INSERT INTO [dbo].[User] ([Username]) VALUES (N'John Doe');
    INSERT INTO [dbo].[User] ([Username]) VALUES (N'John McDoe');
END