USE [ZehuaPan.TaskManagementSystem];
INSERT INTO [Users] ([Email], [Password], [Fullname], [Mobileno]) VALUES ('harrison111pan@gmail.com', '123456', 'Zehua Pan1', '1234567890');
INSERT INTO [Users] ([Email], [Password], [Fullname], [Mobileno]) VALUES ('harrison222pan@gmail.com', '123456', 'Zehua Pan2', '1234567890');
INSERT INTO [Users] ([Email], [Password], [Fullname], [Mobileno]) VALUES ('harrison333pan@gmail.com', '123456', 'Zehua Pan3', '1234567890');


INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (1, 'Feed the fish', 'Give them little fish', '05-18-2021', '3', 'Good to see them grow');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (1, 'Feed the dogs', 'Give them bone', '05-18-2021', '3', 'Good to see them grow');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (1, 'Feed the cats', 'Give them fish', '05-18-2021', '2', 'Good to see them grow');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (1, 'Feed the tigers', 'Give them meat', '05-18-2021', '1', 'Good to see them grow');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (2, 'Clean the table', 'Wash it!', '05-18-2021', '1', 'Nice Job!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (2, 'Clean the window', 'Wash it!', '05-18-2021', '2', 'Nice Job!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (2, 'Clean the bed', 'Flat it', '05-18-2021', '2', 'Nice Job!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (2, 'Clean the computer', 'Drop it', '05-18-2021', '1', 'Nice Job!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (3, 'Create the database', 'Use EF Core and SQL server', '05-18-2021', '1', 'Excellence!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (3, 'Create the repositories', 'Use DbContext and LINQ', '05-18-2021', '1', 'Excellence!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (3, 'Create the services', 'Use Repositories', '05-18-2021', '1',  'Excellence!!');
INSERT INTO [Tasks] ([userid], [Title], [Description], [DueDate], [Priority], [Remarks]) VALUES (3, 'Create the controllers', 'Use services', '05-18-2021', '1', 'Excellence!!');


INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (1, 'Feed the lions', 'Give them meat', '05-18-2021', '05-18-2021', 'Good to see them grow');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (1, 'Feed the rats', 'Give them everything', '05-18-2021', '05-18-2021', 'Good to see them grow');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (1, 'Feed the rabbits', 'Give them carrot', '05-18-2021', '05-18-2021', 'Good to see them grow');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (1, 'Feed the birds', 'Give them water', '05-18-2021', '05-18-2021', 'Good to see them grow');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (2, 'Clean the eyes', 'Wash it!', '05-18-2021', '05-18-2021', 'Nice Job!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (2, 'Clean the heads', 'Wash it!','05-18-2021', '05-18-2021', 'Nice Job!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (2, 'Clean the hands', 'Wash it!','05-18-2021', '05-18-2021', 'Nice Job!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (2, 'Clean the feet', 'Wash it!', '05-18-2021', '05-18-2021', 'Nice Job!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (3, 'Create the component', 'Use ng g c', '05-18-2021', '05-18-2021','Excellence!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (3, 'Create the service', 'Use ng g s', '05-18-2021', '05-18-2021','Excellence!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (3, 'Switch to View', 'Alt + O', '05-18-2021', '05-18-2021','Excellence!!');
INSERT INTO [Tasks History] ([userid], [Title], [Description], [DueDate], [Completed], [Remarks]) VALUES (3, 'Adjust the format', 'Alt + Shift + F ', '05-18-2021', '05-18-2021','Excellence!!');
