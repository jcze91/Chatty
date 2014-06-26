function Department(attributes) {
    this.id = attributes.Id;
    this.name = attributes.Name;
    this.users = [];
    this.setUsers(attributes.Users);

}
Department.prototype.getId = function(){
    return this.id;
};
Department.prototype.setUsers = function (users)
{
    for (var user in users)
        this.users.push(new User(user));
};