function User(attributes){
    this.token = attributes.Token;
    this.userName = attributes.Username;
    this.id = attributes.Id;
    this.firstName = attributes.Firstname;
    this.lastName = attributes.Lastname;
    this.email = attributes.Email;
    this.isBanned = !attributes.isEnable;
    this.connexionDate = attributes.ConnexionDate;
    this.job = attributes.Job;
    this.departmentId = attributes.DepartmentId;
}
User.prototype.getId = function(){
    return this.id;
};
User.prototype.getToken = function(){
    return this.token;
};
User.prototype.setName = function(userName){
    this.userName = userName;
};
User.prototype.getName = function(){
    return this.userName;
};

User.prototype.getFirstName = function(){
    return this.firstName;
};

User.prototype.getLastName = function(){
    return this.lastName;
};

User.prototype.getIsBanned = function(){
    return this.isBanned;
};