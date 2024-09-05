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
            //abone yoksa silinir
            channel.assertQueue(queName,{
                durable:false
            })
           channel.consume(queName,(msg)=>{
                console.log(`Received:${msg.content.toString()}`);
                channel.ack(msg);
                //publisher çalıştığında çalışır.
           });
    });
});
