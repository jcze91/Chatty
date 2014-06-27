function Department(attributes) {
    this.id = attributes.Id;
    this.name = attributes.Name;
    this.users = [];
    this.setUsers(attributes.Users);
    this.title = "Departments";
}
Department.prototype.getId = function(){
    return this.id;
};
Department.prototype.setUsers = function (users)
{
    for (var i = 0; users && i < users.length; i++)
        this.users.push(new User(users[i]));
};