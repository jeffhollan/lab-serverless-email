module.exports = function (context, data) {
    context.log('Webhook was triggered!');

    context.res = {
        body: `<body><p>Hello ${data.Name}<br/>Thank you for signing up on ${data.DateJoined}</p></body>`
    }

    context.done();
}