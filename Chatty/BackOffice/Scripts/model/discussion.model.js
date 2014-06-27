function Discussion(attributes) {
    this.userFromId = attributes.UserFromId;
    this.userToId = attributes.UserToId;
    this.groupId = attributes.GroupId;
    this.groupName = attributes.GroupName;
    this.users = [];
    this.setUsers(attributes.Users);
    this.messages = [];
    this.setMessages(attributes.Messages);
}
Discussion.prototype.getAllNames = function () {
    var ret = "";
    for (var i = 0; this.users && i < this.users.length; i++) {
        ret += this.users[i].userName;
        if (i != this.users.length - 1)
            ret += ", ";
    }
    return ret;
};
Discussion.prototype.setUsers = function (users)
{
    for (var i = 0; users && i < users.length; i++)
        this.users.push(new User(users[i]));
};
Discussion.prototype.setMessages = function (msgs) {
    for (var i = 0; msgs && i < msgs.length; i++)
        this.messages.push(new Message(msgs[i]));
};
Discussion.prototype.getGroupName = function () {
    if (this.groupName)
        return this.groupName + " - ";
};