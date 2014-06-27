function Message(attributes) {
    this.id = attributes.Id;
    this.text = attributes.Content;
    this.userName = attributes.UserName;
    this.date = attributes.Date;
}
Message.prototype.getId = function(){
    return this.id;
};