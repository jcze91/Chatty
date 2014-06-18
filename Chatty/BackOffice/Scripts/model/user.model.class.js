function User(attributes){
    this.token = attributes.token;
    this.userName = attributes.userName ? attributes.userName : null;
    this.id = attributes.id;
    this.messages = attributes.messages? attributes.messages : [];
    this.avatar = attributes.avatar;
    this.firstName = attributes.firstName;
    this.lastName = attributes.lastName;
    this.isBanned = attributes.IsBanned || false;
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

User.prototype.getAvatar = function(){
    return this.avatar;
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