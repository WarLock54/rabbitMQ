const amqp=require('amqplib/callback_api');
amqp.connect('amqp://localhost',(err,con)=>{
    if(err){
        throw err;
    }
    con.createChannel((err,channel)=>{
            if(err){
                throw err;
            }
            let queName="Onur";
            let message="HelloWord";
            //abone yoksa silinir
            channel.assertQueue(queName,{
                durable:false
            })
            channel.sendToQueue(queName,Buffer.from(message));
            console.log(message);
            setTimeout(()=>{
                con.close();
            },1000);
    });
});
